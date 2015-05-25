namespace Errs.WebUi.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using AutoMapper;
    using DependencyResolution;

    public class AutoMapperBootstrapper
    {
        private static readonly object Lock = new object();

        private static bool _initialized;

        public static void Initialize(StructureMapDependencyScope scope)
        {
            lock (Lock)
            {
                if (_initialized)
                {
                    return;
                }

                InitializeInternal(scope);
                _initialized = true;
            }
        }

        private static void InitializeInternal(StructureMapDependencyScope scope)
        {
            Mapper.Initialize(cfg =>
            {
                var profileNames = new List<string>();
                foreach (var profile in scope.GetAllInstances<Profile>())
                {
                    profileNames.Add(profile.ProfileName);
                    cfg.AddProfile(profile);
                }

                cfg.ConstructServicesUsing(scope.GetInstance);
            });
        }
    }
}