using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;

namespace salty.core
{
    public class WorldManager
    {
        public World World;
        public DefaultParallelRunner Runner;
        public SequentialSystem<float> System;

        public WorldManager()
        {
            World = new World();
            Runner = new DefaultParallelRunner(1);
            System = new SequentialSystem<float>();
        }
    }
}

