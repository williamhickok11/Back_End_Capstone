"use strict";

SwapNShop.factory('AuthFactory', ['$rootScope', '$location',

function ($rootScope, $location) {

	let currentUser = null;

	return {
		getUser () {
			// if (currentUser === null){
   //    			$location.path("/login");
   //  		}
			return currentUser;
		},
		setUser (user) {
			currentUser = user;
		}
	}
}
]);

// myServices.factory('Factory', ['$rootScope', '$location', function ($rootScope, $location) {
//   // do something and redirect
//   $location.path('path')
//   $rootScope.$apply()
// }])