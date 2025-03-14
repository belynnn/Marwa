﻿using DAL.Entities;
using System;

namespace BLL.Entities
{
    public class Jeu
    {
        public Guid Jeu_Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public decimal? DurationMinutes { get; set; }
        public DateOnly CreationDate { get; set; }

        // Relation avec les tags via la table de liaison
        public ICollection<JeuxTag> JeuxTags { get; set; } = new List<JeuxTag>();


        // Les tags associés à ce jeu
        public ICollection<Tag> Tags { get; set; }

        // Constructeur qui accepte 9 arguments
        public Jeu(Guid jeu_Id, string nom, string description, int minAge, int maxAge,
            int minPlayers, int maxPlayers, decimal? durationMinutes, DateOnly creationDate)
        {
            Jeu_Id = jeu_Id;
            Nom = nom;
            Description = description;
            MinAge = minAge;
            MaxAge = maxAge;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            DurationMinutes = durationMinutes;
            CreationDate = creationDate;

            // Initialisation des collections
            JeuxTags = new List<JeuxTag>();
            Tags = new List<Tag>();
        }

    }

}

