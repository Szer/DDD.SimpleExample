using System.Collections.Generic;
using System.ComponentModel.Composition;
using DDD.SimpleExample.ReadSide.Interfaces;

namespace DDD.SimpleExample.ReadSide.Helpers
{
    internal class ContextConfiguration
    {
        [ImportMany(typeof(IModelConfiguration))]
        public IEnumerable<IModelConfiguration> Configurations { get; set; }
    }
}