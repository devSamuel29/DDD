using PROJETO.Infra.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PROJETO.Infra.Mapping;

public class UserMap : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder) 
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id);
        builder.Property(u => u.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();
        
        builder.Property(p => p.Role)
            .HasColumnName("Role")
            .HasColumnType("varchar(5)")
            .HasDefaultValue("User")
            .IsRequired();
        
        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(40)")
            .IsRequired();
        
        builder.Property(p => p.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar(40)")
            .IsRequired(); 
        
        builder.Property(p => p.Password)
            .HasColumnName("Password")
            .HasColumnType("varchar(84)")
            .IsRequired();

        builder.Property(p => p.BirthDay)
            .HasColumnName("BirthDay")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime2")
            .IsRequired();
        
        builder.Property(p => p.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime2")
            .IsRequired();
    }
}
