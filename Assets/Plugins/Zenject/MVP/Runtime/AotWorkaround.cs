using UnityEngine.Scripting;

namespace Zenject.MVP
{
    public static class AotWorkaround
    {
        [Preserve]
        private static void Preserve()
        {
            new AlertDialogFactory();
            new AlertDialogPool();
        }
    }
}
