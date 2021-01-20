﻿using System;
namespace NkdApp.ContentMock
{
	public static class HtmlSourceContent
	{
		public static string Content =
			"<html>" +
				"<head>" +
					"<meta charset=\"utf-8\">" +
					"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\">" +
					"<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css\" " +
					"integrity=\"sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh\" crossorigin=\"anonymous\">" +

					"<script type=\"text/javascript\">" +
						"function setresult_takephoto(value) {" +
						"    document.getElementById(\"photoCamera_ResultElement\").src = \"data:image/png;base64,\" + value;" +
						"    document.getElementById(\"photoCamera_placeholderElement\").remove();" +
						"}" +
						"function setresult_selectphoto(value) {" +
						"    document.getElementById(\"photoGallery_ResultElement\").src = \"data:image/png;base64,\" + value;" +
						"    document.getElementById(\"photoGallery_placeholderElement\").remove();" +
						"}" +
						"function setresult_getdeviceinfo(value) {" +
						"    document.getElementById(\"deviceInfo_ResultElement\").innerHTML = value;" +
						"    document.getElementById(\"deviceInfo_placeholderElement\").remove();" +
						"}" +
						"function setresult_getgpslocation(value) {" +
						"    document.getElementById(\"gps_ResultElement\").innerHTML = value;" +
						"    document.getElementById(\"gps_placeholderElement\").remove();" +
						"}" +
						"function setresult_scanqr(value) {" +
						"    document.getElementById(\"qr_ResultElement\").innerHTML = value;" +
						"    document.getElementById(\"qr_placeholderElement\").remove();" +
						"}" +
						"function invokexamarinforms(param){" +
						"    try{" +
						"        invokeXamarinFormsAction(param);" +
						"    }" +
						"    catch(err){" +
						"        alert(err);" +
						"    }" +
						"}" +
					"</script>" +
				"</head>" +

				"<body style=\"background-color: #d4ecff;padding: 20px; border: 1px solid #2196F3;border-radius: 5px;\">" +
					"<div class=\"card border-primary mb-3\">" +
						"<h5 class=\"card-header\">Take Photo</h5>" +
						"<div class=\"card-body\">" +
							"<div class=\"shadow-sm p-2 mb-3 bg-white rounded\">" +
								"<div id=\"photoCamera_placeholderElement\" >" +
									"<span class=\"spinner-grow spinner-grow-sm\" role=\"status\" aria-hidden=\"true\" ></span>" +
									"<span style=\"padding-left:10px;\">Waiting for data...</span>" +
								"</div>" +
								"<img class=\"card-img-top\" id=\"photoCamera_ResultElement\" />" +
							"</div>" +
							"<button type=\"button\" class=\"btn btn-primary btn-lg btn-block\" onclick=\"invokexamarinforms('PHOTO|CAMERA')\">Get from Xamarin.Forms</button>" +
						"</div>" +
					"</div>" +

					"<div class=\"card border-primary mb-3\">" +
						"<h5 class=\"card-header\">Select Photo</h5>" +
						"<div class=\"card-body\">" +
							"<div class=\"shadow-sm p-2 mb-3 bg-white rounded\">" +
								"<div id=\"photoGallery_placeholderElement\" >" +
									"<span class=\"spinner-grow spinner-grow-sm\" role=\"status\" aria-hidden=\"true\" ></span>" +
									"<span style=\"padding-left:10px;\">Waiting for data...</span>" +
								"</div>" +
								"<img class=\"card-img-top\" id=\"photoGallery_ResultElement\" />" +
							"</div>" +
							"<button type=\"button\" class=\"btn btn-primary btn-lg btn-block\" onclick=\"invokexamarinforms('PHOTO|GALLERY')\">Get from Xamarin.Forms</button>" +
						"</div>" +
					"</div>" +

					"<div class=\"card border-primary mb-3\">" +
						"<h5 class=\"card-header\">Device Info</h5>" +
						"<div class=\"card-body\">" +
							"<div class=\"shadow-sm p-2 mb-3 bg-white rounded\">" +
								"<div id=\"deviceInfo_placeholderElement\" >" +
									"<span class=\"spinner-grow spinner-grow-sm\" role=\"status\" aria-hidden=\"true\" ></span>" +
									"<span style=\"padding-left:10px;\">Waiting for data...</span>" +
								"</div>" +
								"<p class=\"text-uppercase\" id=\"deviceInfo_ResultElement\" />" +
							"</div>" +
							"<button type=\"button\" class=\"btn btn-primary btn-lg btn-block\" onclick=\"invokexamarinforms('DEVICEINFO')\">Get from Xamarin.Forms</button>" +
						"</div>" +
					"</div>" +

					"<div class=\"card border-primary mb-3\">" +
						"<h5 class=\"card-header\">GPS Location</h5>" +
						"<div class=\"card-body\">" +
							"<div class=\"shadow-sm p-2 mb-3 bg-white rounded\">" +
								"<div id=\"gps_placeholderElement\" >" +
									"<span class=\"spinner-grow spinner-grow-sm\" role=\"status\" aria-hidden=\"true\" ></span>" +
									"<span style=\"padding-left:10px;\">Waiting for data...</span>" +
								"</div>" +
								"<p class=\"text-uppercase\" id=\"gps_ResultElement\" />" +
							"</div>" +
							"<button type=\"button\" class=\"btn btn-primary btn-lg btn-block\" onclick=\"invokexamarinforms('GPS')\">Get from Xamarin.Forms</button>" +
						"</div>" +
					"</div>" +

					"<div class=\"card border-primary mb-3\">" +
						"<h5 class=\"card-header\">QR Scan</h5>" +
						"<div class=\"card-body\">" +
							"<div class=\"shadow-sm p-2 mb-3 bg-white rounded\">" +
								"<div id=\"qr_placeholderElement\" >" +
									"<span class=\"spinner-grow spinner-grow-sm\" role=\"status\" aria-hidden=\"true\" ></span>" +
									"<span style=\"padding-left:10px;\">Waiting for data...</span>" +
								"</div>" +
								"<p class=\"text-uppercase\" id=\"qr_ResultElement\" />" +
							"</div>" +
							"<button type=\"button\" class=\"btn btn-primary btn-lg btn-block\" onclick=\"invokexamarinforms('QR')\">Get from Xamarin.Forms</button>" +
						"</div>" +
					"</div>" +

				"</body>" +
			"</html>";
	}
}
