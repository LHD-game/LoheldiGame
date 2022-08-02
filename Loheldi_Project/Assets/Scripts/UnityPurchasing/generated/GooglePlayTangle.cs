// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("yjwYEUXvTnpxwlpFA40IVOmux9Wo2bc3cZQz3AUTMEHUYBl90cA46Dq5t7iIOrmyujq5ubgS5d+nnSpq3pQdzhatWdSNei4ME0FrLg2C307sTa3DpQmdeyjE6/nNM2sWcK07pN+m5ZbGtk3A1ZYMOgMwrXGm/mAkA/y/NdkSm3dYDzHfpunz53A+yylfPnKG9LPYo7vzT8eITWiVkrZ8fYg6uZqItb6xkj7wPk+1ubm5vbi79GozxmNGGDVg7v2kYOdQylVAKeFF630Y0J6L/GVqvSJzTJpNkH8VEkL/jpPqvWzu3ydFVQJ/+y8OMxjyne6YrBwQvTZBIiMGhmfD8jfMeYyHy/ayFqVfN9QQxwG11f92xMtXHvNS7B6bJck3Gbq7ubi5");
        private static int[] order = new int[] { 6,3,3,12,12,7,8,12,8,10,11,11,12,13,14 };
        private static int key = 184;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
