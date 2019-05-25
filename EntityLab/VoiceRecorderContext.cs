using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLab
{
    class VoiceRecorderContext : DbContext
    {
        
        public VoiceRecorderContext()
            : base("DbConnection")
        { }

        public DbSet <VoiceRecorder> Notes { get; set; }
    }
}

