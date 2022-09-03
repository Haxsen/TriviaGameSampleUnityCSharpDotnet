using UnityEngine;

using ViewModel;
using UnityEngine.SceneManagement;

namespace MVVM.ViewModel
{
	public class SceneManagerWrapper : ISceneManager
	{
		public AsyncOperation LoadSceneAsync(int sceneBuildIndex) => SceneManager.LoadSceneAsync(sceneBuildIndex);
	}
}
