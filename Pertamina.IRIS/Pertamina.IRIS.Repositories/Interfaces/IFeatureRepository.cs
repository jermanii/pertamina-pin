using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IFeatureRepository
    {
        FeatureViewModel GetFeatureById(string guid);
    }
}
