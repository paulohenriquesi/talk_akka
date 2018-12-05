using Akka.Actor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TalkAkka.Northwind;
using TalkAkka.SingleActors.Messages;

namespace TalkAkka.SingleActors.Actors
{
    public class WriteFileActor : ReceiveActor
    {
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
            var fileContent = JsonConvert.SerializeObject(_file);
            File.WriteAllText($@"C:\github\paulohenriquesi\talk_akka\files\{DateTime.Now:yyyy-MM-ddTHH-mm-ss}customers.json", fileContent);
            _file.Clear();

            Context.Sender.Tell(msg);
        }
    }
}
