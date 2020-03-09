using System;
using System.Collections.Generic;
using System.Text;

namespace cw2
{
    class OwnComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer.InvariantCultureIgnoreCase
                .Equals($"{x.FirstName} {x.SecondtName} {x.IndexNumber}",
                $"{y.FirstName} {y.SecondtName} {y.IndexNumber}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer.CurrentCultureIgnoreCase
                .GetHashCode($"{obj.FirstName} {obj.SecondtName} {obj.IndexNumber}");
        }
    }
}
