"use strict";
 // console.log("got here")

SwapNShop.controller("registerCtrl", [
  "$scope",
  "$http",

	function ($http, $scope){
		  $scope.githubOauth = function () {
		  	// console.log("go")
		  	//first thing Oauth provides
		  	OAuth.initialize('GSm3m9Fh7tFR1U8kiEspvMlN6k4')
			OAuth.popup('github').done(function(result) {
			    console.log(result)

			    // do some stuff with result
			    result.me().done(function(data) {
			    	console.log(data);

			    	$http({
			    		url: "http://localhoast:5000/api/Geek",
			    		method: "POST",
			    		data: JSON.stringify({
			    			username: data.alias,
			    			location: data.location,
			    			emailAddress: data.email,
			    			createdDate: new Date()
			    		})
			    	})
				})
			});
		};
	}
]);
