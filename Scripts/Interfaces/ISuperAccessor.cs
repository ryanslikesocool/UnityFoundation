using UnityEngine;

namespace Foundation {
    public interface ISuperAccessor<Super> where Super : class {
        Super super { get; set; }
    }
}