using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace WinService.Service
{
    class WinService
    {

        public ILog Log { get; private set; }
        private System.Timers.Timer mytimer;
        public WinService(ILog logger)
        {

            // IocModule.cs needs to be updated in case new paramteres are added to this constructor
            this.mytimer = new System.Timers.Timer();
            //((System.ComponentModel.ISupportInitialize)(this.mytimer)).BeginInit();

            //this.mytimer.Enabled = true;
            //this.mytimer.Interval = 2000D;
           // this.mytimer.Elapsed += new System.Timers.ElapsedEventHandler(this.mytimer_Elapsed);
          //  ((System.ComponentModel.ISupportInitialize)(this.mytimer)).EndInit();

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            Log = logger;

        }

        public bool Start(HostControl hostControl)
        {

            Log.Info($"{nameof(Service.WinService)} Start command received.");

            this.mytimer.Elapsed += new System.Timers.ElapsedEventHandler(mytimer_Elapsed);
            mytimer.Interval = 5000;
            mytimer.Enabled = true;
            

            //TODO: Implement your service start routine.
            return true;

        }

        public bool Stop(HostControl hostControl)
        {

            Log.Trace($"{nameof(Service.WinService)} Stop command received.");

            //TODO: Implement your service stop routine.
            return true;

        }

        public bool Pause(HostControl hostControl)
        {

            Log.Trace($"{nameof(Service.WinService)} Pause command received.");

            //TODO: Implement your service start routine.
            return true;

        }

        public bool Continue(HostControl hostControl)
        {

            Log.Trace($"{nameof(Service.WinService)} Continue command received.");

            //TODO: Implement your service stop routine.
            return true;

        }

        public bool Shutdown(HostControl hostControl)
        {

            Log.Trace($"{nameof(Service.WinService)} Shutdown command received.");

            //TODO: Implement your service stop routine.
            return true;

        }



        private void mytimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Log.Info("Timmer");
        }

    }
}
