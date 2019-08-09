using ArtistAwards.DAL.Domain;
using System.Collections.Generic;

namespace ArtistAwards.DAL.Interface
{
    public interface IAwardService
    {
        IEnumerable<Award> GetAll();

        Award GetById(int id);

        void Update(Award obj);

        void Delete(int id);
    }
}
