using UnityEngine;

namespace MiniGame3
{
    public class VolleyBallPlayer : MonoBehaviour
    {
        [SerializeField] Team team;

        public Team GetTeam()
        {
            return team;
        }
    }
}
