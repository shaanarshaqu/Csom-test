using Microsoft.SharePoint.Client;
using TestCsom.DTOs;
using TestCsom.Manager.Interfaces;
using TestCsom.Secure._365_Auth;

namespace TestCsom.Manager
{
    public class StudentManager:IStudentManager
    {
        private readonly ClientContext context;
        private readonly IConfiguration configuration;
        private readonly MicrosoftAuth microsoftAuth;
        public StudentManager(IConfiguration configuration, MicrosoftAuth microsoftAuth)
        {
            this.configuration = configuration;
            this.context = new ClientContext(configuration["SharepointInfo:SiteUrl"]);
            this.microsoftAuth=microsoftAuth;
            context.ExecutingWebRequest += (sender, args) =>
            {
                args.WebRequestExecutor.RequestHeaders["Authorization"] =
                    "Bearer " + microsoftAuth.GetAccessTokenAsync();
            };
        }

        public async Task<dynamic> GetAllData()
        {
            try
            {                
                List studentList = context.Web.Lists.GetByTitle("Students");
                CamlQuery query = new CamlQuery
                {
                    ViewXml = @"
                    <View>
                        <Query>
                        </Query>
                        <RowLimit>100</RowLimit>
                    </View>"
                };

                // Get student list items
                ListItemCollection studentItems = studentList.GetItems(query);
                context.Load(studentItems);
                await context.ExecuteQueryAsync();

                var result = new List<StudentDto>();

                foreach (ListItem studentItem in studentItems)
                {
                    var student = new StudentDto
                    {
                        Id = (int)studentItem["ID"],
                        Title = studentItem["Title"]?.ToString()
                    };

                    if (studentItem["Dep_Id"] != null)
                    {
                        FieldLookupValue departmentLookup = (FieldLookupValue)studentItem["Dep_Id"];

                        // Get the department name from the "department" list
                        List departmentList = context.Web.Lists.GetByTitle("Department");
                        ListItem departmentItem = departmentList.GetItemById(departmentLookup.LookupId);
                        context.Load(departmentItem);
                        await context.ExecuteQueryAsync();

                        student.DepartmentName = departmentItem["Title"]?.ToString();
                    }

                    result.Add(student);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        
    }
}
