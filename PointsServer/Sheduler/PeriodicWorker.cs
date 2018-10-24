using PointsServer.Models;
using PointsServer.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace PointsServer.Sheduler
{
    public class PeriodicWorker
    {
        private Thread _thread;
        private const int _interval = 10000;

        public PeriodicWorker(Func<double, double> func)
        {
            _thread = new Thread(() => ThreadFunc(func))
            {
                IsBackground = true,
                Name = "ThreadFunc"
            };
        }

        public void Start()
        {
            _thread.Start();
        }

        private void ThreadFunc(Func<double, double> func)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Elapsed += (sender, args) => TimerWorker(sender, func);
            t.Interval = _interval;
            t.Enabled = true;
            t.AutoReset = true;
            t.Start();
        }

        private void TimerWorker(object sender, Func<double, double> func)
        {
            var currentTime = DateTime.Now;
            var timeInterval = currentTime - DateTime.MinValue;
            var t = (timeInterval.TotalSeconds % 1000) / 100;
            var value = func(t);

            var dbController = new MongoController(new MongoAdapter());
            var result = dbController.InsertPoint(new Point()
            {
                Time = currentTime,
                Value = value
            });
        }
    }
}