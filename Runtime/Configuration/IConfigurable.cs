using UnityEngine;

namespace Foundation {
    public interface IConfigurable<Configuration> where Configuration : IConfiguration {
        Configuration configuration { get; }
    }
}