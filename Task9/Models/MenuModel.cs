using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task9
{
    internal class MenuModel : IEnumerable
    {
        private List<DishModel> _dishes;
        public DishModel this[int index] => _dishes[index];
        public int Length => _dishes.Count;
        public MenuModel()
        {
            _dishes = new List<DishModel>();
        }
        public MenuModel(List<DishModel> dishes) : this()
        {
            foreach (DishModel dish in dishes)
            {
                _dishes.Add(new(dish));
            }
        }
        public void Add(DishModel dish)
        {
            _dishes.Add(dish);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dishes.GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DishModel dish in _dishes)
            {
                stringBuilder.AppendLine(dish.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
