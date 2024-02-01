using Newtonsoft.Json;

namespace Gradeo.CoreApplication.Domain.Entities;

public class GradeModel
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("grade")]
    public decimal Grade { get; set; }
    
    [JsonProperty("studyGroupId")]
    public int StudyGroupId { get; set; }
    [JsonProperty("subjectId")]
    public int SubjectId { get; set; }
    [JsonProperty("studentId")]
    public int StudentId { get; set; }
    [JsonProperty("date")]
    public DateTimeOffset Date { get; set; }
    [JsonProperty("schoolId")]
    public int SchoolId { get; set; }
    [JsonProperty("createdBy")]
    public string CreatedBy { get; set; }
    [JsonProperty("lastModifiedBy")]
    public string? LastModifiedBy { get; set; }
    [JsonProperty("createdDate")]
    public DateTimeOffset CreatedDate { get; set; }
    [JsonProperty("lastModifiedDate")]
    public DateTimeOffset? LastModifiedDate { get; set; }
}