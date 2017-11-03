using System.Threading.Tasks;

namespace SkeletaWeb.Services
{
	public interface IServices
	{
		Task<int> GetAverageCustomerAge();
	}
}