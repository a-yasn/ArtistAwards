using ArtistAwards.DAL.Domain;
using ArtistAwards.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtistAwards.DAL.Service
{
    public class AwardService : IAwardService
    {
        private static readonly List<Award> source = GenerateRandomData();

        private static List<Award> GenerateRandomData()
        {
            List<Award> items = new List<Award>();

            for (int num = 1; num < 50; num++)
            {
                var year = DateTime.Now.AddYears(-1 * num).Year;
                items.Add(new Award
                {
                    Id = num,
                    Year = year,
                    Name = $"The Oscar of {year} year",
                    Role = $"The role of first plane",
                    Show = $"Show of Harry Truman {num}"
                });
            }

            return items;
        }

        public IEnumerable<Award> GetAll()
        {
            return source;
        }

        public Award GetById(int id)
        {
            return source.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(Award obj)
        {
            if (obj.Id == 0)
            {
                source.Add(obj);
            }
            else
            {
                var val = GetById(obj.Id);

                if (val == null)
                {
                    return;
                }

                val.Name = obj.Name;
                val.Show = obj.Show;
                val.Role = obj.Role;
                val.Year = obj.Year;
            }
        }

        public void Delete(int id)
        {
            var obj = GetById(id);

            if (obj == null)
            {
                return;
            }

            source.Remove(obj);
        }
    }
}
