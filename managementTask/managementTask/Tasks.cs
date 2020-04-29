﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace managementTask
{
    public struct Task
    {
        public int Task_ID { get; set; }
        public int User_ID { get; set; }
        public string Tip { get; set; }
        public string Status { get; set; }
        public string Continut { get; set; }
        public int Nota { get; set; }
        public int TimpEstimat { get; set; }

        public Task(int task_ID, int user_ID, string tip, string status, string continut, int nota, int timpEstimat)
        {
            Task_ID = task_ID;
            User_ID = user_ID;
            Tip = tip;
            Status = status;
            Continut = continut;
            Nota = nota;
            TimpEstimat = timpEstimat;
        }
    }
    public class Tasks
    {
        private static List<Task> _tasks;
        private Packet packet = new Packet();
        private Packet response = new Packet();

        public static List<Task> MyTasks
        {
            get
            {
                return _tasks;
            }
        }
        public Tasks(Client client)
        {
            try
            {
              
                _tasks = new List<Task>();

                //type=1 pentru ca cer date
                packet._type = "1";
                packet._idClient = client.id;
                packet._data = "GetTable|TaskDB,Task,task";

                client.WriteObject(packet);
                response = client.ReadObject();

                //parsez stringul
                string[] values = response._data.Split(';').ToArray();
                foreach (string str in values)
                {
                    string[] toks = str.Split(',').ToArray();
                   
                    if (!str.Equals(""))
                    {
                        Task task = new Task(Convert.ToInt32(toks[0]), Convert.ToInt32(toks[1]), toks[2], toks[3], toks[4], Convert.ToInt32(toks[5]), Convert.ToInt32(toks[6]));
                        _tasks.Add(task);
                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
