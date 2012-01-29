//-------------------------------------------------------------------------------
// <copyright file="NinjectAbstractServiceHost.cs" company="Ninject Project Contributors">
//   Copyright (c) 2009-2011 Ninject Project Contributors
//   Author: Remo Gloor (remo.gloor@gmail.com)
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   you may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Ninject.Extensions.Wcf
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    /// <summary>
    /// Abstract base class for WebServiceHost that initializes based on the
    /// ServiceBehavior attribute as singleton or multi instance service
    /// </summary>
    /// <typeparam name="T">The type of the service</typeparam>
    public abstract class NinjectAbstractServiceHost<T> : NinjectServiceHost
    {
        /// <summary>
        /// The ninject kernel
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the NinjectAbstractServiceHost class.
        /// </summary>
        /// <param name="serviceBehavior">The service behavior.</param>
        /// <param name="baseAddresses">The baseAddresses.</param>
        /// <param name="kernel">The kernel.</param>
        protected NinjectAbstractServiceHost(IServiceBehavior serviceBehavior, Uri[] baseAddresses, IKernel kernel)
            : base(serviceBehavior)
        {
            this.kernel = kernel;
            var addresses = new UriSchemeKeyedCollection(baseAddresses);

            if (ServiceTypeHelper.IsSingletonService<T>())
            {
                this.InitializeDescription(this.kernel.Get<T>(), addresses);
            }
            else
            {
                this.InitializeDescription(typeof(T), addresses);
            }
        }        
    }
}