"use strict";

SwapNShop.controller('registerCtrl', [
	'$http', 
	'$scope',
	'$location',
	'AuthFactory',

	function ($http, $scope, $location, AuthFactory) {

		$scope.dataFromFireUser = {};
		//Non Github oAuth
		var ref = new Firebase("https://swapnshop.firebaseio.com/");
		$("#sign_up_BTN").on('click', ()=>{
			//Define user name and psWord

			ref.createUser({
			    email    : $scope.dataFromFireUser.email,
			    password : $scope.dataFromFireUser.password
		  	}, function(error, userData) {
			    if (error) {
			      	console.log("Error creating user:", error);
			    } else {
				    console.log("Successfully created user account with uid:", userData.uid);
			    }
		  	});
		  	$location.path("/homepage");
		  	createFireBaseMusician($scope.dataFromFireUser);
		})


		$("#login_up_BTN").on('click', ()=>{
		  	$scope.dataFromFireUser = {};

		  	ref.authWithPassword({
		    	email    : $scope.dataFromFireUser.email,
			    password : $scope.dataFromFireUser.password
		  	}, function(error, authData) {
		    	if (error) {
		      		codeonsole.log("Login Failed!", error);
		    	} else {
		      		// Write code to allow user to access the website
		      		console.log("Authenticated successfully with payload:", authData);
		    	}
		  	});
		  	$location.path("/homepage");
		  	createFireBaseMusician($scope.dataFromFireUser);
		});


		//Github oAuth
		$scope.githubOauth = function () {
			OAuth.initialize('GSm3m9Fh7tFR1U8kiEspvMlN6k4');

			OAuth.popup('github').done(function(result) {
			    console.log(result)

				result.me().done(function(data) {
				    // do something with `data`, e.g. print data.name
				    console.log(data);
				    // Call the funtion to create the user
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

		// Function to create user with Firebase data
		function createFireBaseMusician(data) {
			console.log("dataaaaaa",data);
			$http({
			    	url: "http://localhost:49881/api/Musician",
			    	method: "POST",
			    	data: JSON.stringify({
			    		userName: data.name,
			    		state: data.state,
			    		city: data.city,
			    		email: data.email
			    	})
			    })
			    .then(
			    response => {
			    	let theMusician = response.data;
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
			    					let theMusician = response.data;
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
		}
	}
]);