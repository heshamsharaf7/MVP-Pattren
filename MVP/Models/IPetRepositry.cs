﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP.Models
{
   public interface IPetRepositry
    {
        void Add(PetModel petModel);
        void Edit(PetModel petModel);
        void Delete(int id);
        IEnumerable<PetModel> GetAll();
        IEnumerable<PetModel> GetByValue(string value);//search
    }
}
