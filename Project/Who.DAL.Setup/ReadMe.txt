Section: Guidelines
	When renaming a section name, do not forget to check code for references for the old section name

Section: Test Url for valid picture
	If you need to test a picture, the following url should be valid;
	"https://graph.microsoft.com/v1.0/users/9b95b4f8-9947-4647-824e-6f8e6ce22ab3/photo/$value";
	(url works as of 23/09/2018)
                
Section: How to get a valid bearer token;
	go to url; https://developer.microsoft.com/en-us/graph/graph-explorer
    log in, do a request (e.g. https://graph.microsoft.com/v1.0/users/9b95b4f8-9947-4647-824e-6f8e6ce22ab3/photo/$value
    track the request (in the dev-tab; F12 for Chrome),
	copy the bearer token and paste it in the HttpHeaderSettings file
                    