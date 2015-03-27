using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DeadlockExample.Startup))]

namespace DeadlockExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
