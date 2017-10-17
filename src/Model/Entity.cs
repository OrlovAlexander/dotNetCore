using System;

namespace Model
{
    public class Entity : IEntity
    {
        public int Id { get; private set; }

        public Entity(int id)
        {
            this.Id = id;
        }

        private Entity() {}
    }
}