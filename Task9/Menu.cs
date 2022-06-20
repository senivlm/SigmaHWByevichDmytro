using System.Collections.Generic;

namespace Task9
{
    internal class Menu
    {
        private List<Dish> _dishes;
        public Dish this[int index] => _dishes[index];
        public int Length => _dishes.Count;
        public Menu()
        {
            _dishes = new List<Dish>();
        }
        public Menu(List<Dish> dishes) : this()
        {
            _dishes = dishes;
        }
    }
}
