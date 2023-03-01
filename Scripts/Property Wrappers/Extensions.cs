using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        public static void Mutate<Value>(this IMutablePropertyWrapper<Value> propertyWrapper, IMutablePropertyWrapper<Value>.MutateAction body)
            => propertyWrapper.Mutate(body);
    }
}