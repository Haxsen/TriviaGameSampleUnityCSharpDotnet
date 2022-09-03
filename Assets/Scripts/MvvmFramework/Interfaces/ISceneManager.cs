using UnityEngine;

namespace ViewModel
{
	public interface ISceneManager
	{
		AsyncOperation LoadSceneAsync(int sceneBuildIndex);
	}
}
