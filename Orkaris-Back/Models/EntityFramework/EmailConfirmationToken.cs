using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Orkaris_Back.Models.EntityFramework;

[Table("t_email_confirmation_tokens_ect")]
public class EmailConfirmationToken
{

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("ect_id")]
    public Guid Id { get; set; }
    [Required, MaxLength(100), Column("ect_token")]
    public string? Token { get; set; }

    [ForeignKey("User"), Required, Column("usr_id")]
    public Guid UserId { get; set; }
    [Column("ect_expiration_date")]
    public DateTime ExpirationDate { get; set; }
    [Required, Column("ect_is_used")]
    public bool IsUsed { get; set; } = false;

    // Navigation property
    [ForeignKey("UserId"), InverseProperty(nameof(User.EmailUser))]
    public User? UserEmail { get; set; }
}
