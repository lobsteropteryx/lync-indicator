using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace LyncIndicator
{
    public partial class LyncPresenceService : ServiceBase
    {
        private LyncStatusWatcher lyncStatusWatcher;

        public LyncPresenceService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.lyncStatusWatcher = new LyncStatusWatcher();
        }

        protected override void OnStop()
        {
        }
    }
}
