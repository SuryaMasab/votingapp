using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using VoterApp.Domain.Models;

namespace VotersApi.Database;

public class VoteAppDbContext:DbContext
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Voter> Voters { get; set; }
    public DbSet<Vote> Votes { get; set; }

    public VoteAppDbContext(DbContextOptions<VoteAppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Voter>(o =>
        {
            o.HasKey(v => v.Id);
            o.Property(v => v.Id).ValueGeneratedOnAdd();
            o.Property(v => v.Name).IsRequired().HasMaxLength(50);
            o.Property(v => v.HasVoted).HasDefaultValue(false);
           
            o.HasMany(v => v.Votes)
                .WithOne(vote => vote.Voter)
                .HasForeignKey(vote => vote.VoterId);
        });

        modelBuilder.Entity<Voter>(o =>
        {
            o.HasKey(c => c.Id);
            o.Property(c => c.Id).ValueGeneratedOnAdd();
            o.Property(c => c.Name).IsRequired().HasMaxLength(50);
            
            //o.HasMany(c => c.Votes)
            //    .WithOne(vote => vote.Candidate)
            //    .HasForeignKey(vote => vote.CandidateId);
        });

        modelBuilder.Entity<Vote>((Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Vote>>)(o =>
        {
            o.HasKey(v => new { v.VoterId, v.CandidateId });

            // Configure the many-to-one relationships
            o.HasOne<Voter>(v => v.Voter)
                .WithMany(voter => (IEnumerable<Vote>?)voter.Votes)
                .HasForeignKey((System.Linq.Expressions.Expression<Func<Vote, object?>>)(v => v.VoterId));

            //o.HasOne<Voter>(v => v.Candidate)
            //    .WithMany(candidate => (IEnumerable<Vote>?)candidate.Votes)
            //    .HasForeignKey((System.Linq.Expressions.Expression<Func<Vote, object?>>)(v => v.CandidateId));
        }));

    }
}
