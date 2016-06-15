"use strict";

SwapNShop.controller('registerCtrl', [
	'$http', 
	'$scope',
	'$location',
	'AuthFactory',

	function ($http, $scope, $location, AuthFactory) {

		$scope.githubOauth = function () {
			OAuth.initialize('GSm3m9Fh7tFR1U8kiEspvMlN6k4');

			OAuth.popup('github').done(function(result) {
			    console.log(result)

				result.me().done(function(data) {
				    // do something with `data`, e.g. print data.name
				    console.log(data);

				    $http({
				    	url: "http://localhost:49881/api/Musician",
				    	method: "POST",
				    	data: JSON.stringify({
				    		userName: data.alias,
				    		state: data.location,
				    		city: data.location,
				    		email: data.email
				    	})
				    })
				    .then(
				    response => {
				    	let theMusician = response.data[0];
				    	AuthFactory.setUser(theMusician);
				    	console.log("resolve fired", theMusician);
				    },
				    response => {
				    	console.log("reject fired", response);

				    	// Musician has already been created
				    	if (response.status === 409) {
				    		$http
				    			.get(`http://localhost:49881/api/Musician?username=${data.alias}`)
				    			.then(
				    				response => {
				    					let theMusician = response.data[0];
				    					console.log("Found the Musician", theMusician);
				    					AuthFactory.setUser(theMusician)
				    				},
				    				response => console.log("Could not find that Musician", response)
				    			)
				    	}
				    })
				    .then(function() {
		          $location.path("/homepage");
		        })
				})
			});
		};
	}
]);






