// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace weatherdisplaytest.DependencyResolution
{
    using AutoMapper;
    using StructureMap;
    using System;
    using System.Linq;

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {



            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
                });

            var profiles = from t in typeof(DefaultRegistry).Assembly.GetTypes()
                           where typeof(Profile).IsAssignableFrom(t)
                           select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            var mapper = config.CreateMapper();

            For<IMapper>().Use(mapper);
        }

        #endregion
    }
}