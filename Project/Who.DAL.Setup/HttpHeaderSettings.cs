namespace Who.DAL.Setup
{
    internal static class HttpHeaderSettings
    {
        internal static class Accept
        {
            internal const string NAME = "Accept";
            internal const string VALUE = "application/json";
        }
        internal static class AcceptEncoding
        {
            internal const string NAME = "Accept-Encoding";
            internal const string VALUE = ""; // Empty because WebClient has issues with decoding certain types... 
        }
        internal static class AcceptLanguage
        {
            internal const string NAME = "Accept-Language";
            internal const string VALUE = "nl-NL,nl;q=0.9,en-US;q=0.8,en;q=0.7";
        }
        internal static class Authorization
        {
            /*
             ReadMe.txt
             The value here needs to be changed following the guides in the ReadMe.txt
             Section: How to get a valid bearer token
                 */
            internal const string NAME = "Authorization";
            internal const string VALUE = "Bearer eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFRQUJBQUFBQUFEWHpaM2lmci1HUmJEVDQ1ek5TRUZFc2RRTjJaUllsd0xMdHBfNEpidDZ2WDBoVk9HQzBjSENhaEoyV0ozV3d0UEp2WjEza1hpVEdlYlpNdVJONEh5VzFEZFYzMHE1TEs1VU1NanRXbVFxaWlBQSIsImFsZyI6IlJTMjU2IiwieDV0IjoiaTZsR2szRlp6eFJjVWIyQzNuRVE3c3lISmxZIiwia2lkIjoiaTZsR2szRlp6eFJjVWIyQzNuRVE3c3lISmxZIn0.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9lZGQxYzNiNi1iZTg3LTQzYmItOTJkNC03YTkxMWM1Y2VlMTcvIiwiaWF0IjoxNTM3NzI0MzcyLCJuYmYiOjE1Mzc3MjQzNzIsImV4cCI6MTUzNzcyODI3MiwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhJQUFBQUxGaGhCZUtBU3dlUktmSVBpVUVOVjJsVnpWblJRNHVOVTBJSVhkb1oxV0tmUFpLRjJnSzg5dVJJOExROFpKa24rb21STXFrVDZaSi9zanBXR2tGcXlCTnU2THJkNTBvMkRhTmwwU21UTnBVPSIsImFtciI6WyJ3aWEiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiR3JhcGggZXhwbG9yZXIiLCJhcHBpZCI6ImRlOGJjOGI1LWQ5ZjktNDhiMS1hOGFkLWI3NDhkYTcyNTA2NCIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiTGVmZXZlci1UZXVnaGVscyIsImdpdmVuX25hbWUiOiJUaG9tYXMiLCJpcGFkZHIiOiI5NC4yMjUuMjMzLjEzIiwibmFtZSI6IlRob21hcyBMZWZldmVyLVRldWdoZWxzIiwib2lkIjoiMDM0MDFlYzMtZDQ5Ny00NjlhLWI4MDMtMDkyMDE3NTIwODE4Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTI5MTU3OTMwNzItMzg4Mzg2ODYxNy0yNTE5OTgzMTkzLTI3NzUiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwM0JGRkRBRDE1RTQ3OSIsInNjcCI6IkNhbGVuZGFycy5SZWFkV3JpdGUgQ29udGFjdHMuUmVhZFdyaXRlIEZpbGVzLlJlYWRXcml0ZS5BbGwgTWFpbC5SZWFkV3JpdGUgTm90ZXMuUmVhZFdyaXRlLkFsbCBvcGVuaWQgUGVvcGxlLlJlYWQgcHJvZmlsZSBTaXRlcy5SZWFkV3JpdGUuQWxsIFRhc2tzLlJlYWRXcml0ZSBVc2VyLlJlYWRCYXNpYy5BbGwgVXNlci5SZWFkV3JpdGUgZW1haWwiLCJzdWIiOiJmZDc2S3Fvd08zNXFxMGwtbkxXN255STNCM29DR3dkYXE1YmwwRnkxWTA0IiwidGlkIjoiZWRkMWMzYjYtYmU4Ny00M2JiLTkyZDQtN2E5MTFjNWNlZTE3IiwidW5pcXVlX25hbWUiOiJUaG9tYXMuTGVmZXZlci1UZXVnaGVsc0BheHhlcy5jb20iLCJ1cG4iOiJUaG9tYXMuTGVmZXZlci1UZXVnaGVsc0BheHhlcy5jb20iLCJ1dGkiOiJLOTY1UzVJdFAwU0tYa2dzM2h3bEFBIiwidmVyIjoiMS4wIiwieG1zX3N0Ijp7InN1YiI6Im80bW1TX3Zsa2duc2J0MWQzcXh0NVFERFlkaXczVGtjWnF5UmhiX3llLTgifSwieG1zX3RjZHQiOiIxNDczOTI1MjM3In0.Kj21-1c3iK8RteEauznNUu6AH8iClOtVnaDkL_zLEYCir58koC-La50aMT0KdW2OXTPAY2xLnK70KmzmdkBvb3QmplxQNtei3-As17nhZnNGH8MqXFeGTPrqyQlratuh-XC2gnQ3ozG4CaZ0nAoRc6vevluB0hIEKjmrlEf39mgI3wqXfl97X3NofCwFIvKQhgBB16XWREyagpQqLmO_C1R9htG9oKhwBH7VJMZ-pCStejpjlHDN3zaftLZVW6IdrGX0gKehjADYq6L-rNQTZjmS6i3fYpsKS4jTQ5U_4rDnV53K8u1YvyRZKvCuqhnydQhpxKKeJXAdZnrb1PI0gw";
        }
    }
}
