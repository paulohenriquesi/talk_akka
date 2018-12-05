using System;
using System.Collections.Generic;
using System.IO;
using Akka.Actor;
using Newtonsoft.Json;
using TalkAkka.GroupActors.Messages;
using TalkAkka.Northwind;

namespace TalkAkka.GroupActors.Actors
{
    public class WriteFileActor : ReceiveActor
    {
        private int _finisheds;
        private readonly List<CustomerWithOrder> _file;

        public WriteFileActor()
        {
            _file = new List<CustomerWithOrder>();

            Become(Waiting);
        }

        private void Waiting()
        {
            Receive<AppendToFile>(x => Become(Accumulating));
        }

        private void Accumulating()
        {
            Receive<AppendToFile>(x => Append(x));
            Receive<Finished>(x => Write(x));
        }

        private void Append(AppendToFile msg)
        {
            _file.Add(msg.CustomerWithOrder);
            Become(Accumulating);
        }

        private void Write(Finished msg)
        {
            _finisheds++;

            if (_finisheds == 10)
            {
                var fileContent = JsonConvert.SerializeObject(_file);
                File.WriteAllText($@"C:\github\paulohenriquesi\talk_akka\files\{DateTime.Now:yyyy-MM-ddTHH-mm-ss}customers.json", fileContent);
                _file.Clear();

                _finisheds = 0;

                Context.Sender.Tell(msg);
            }
        }
    }
}
