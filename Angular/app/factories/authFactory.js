"use strict";

SwapNShop.factory('AuthFactory', [

function () {

	// isAuthenticated () {
 //      	// authenticate the user (all the date associated with the current user)
 //      	let authData = ref.getAuth();
 //      	console.log("authData", authData);
 //      	if (authData) {
 //       		return true;
 //      	} else {
 //        	return false;
 //      	}
 //    },

	let currentUser = null;

	return {
		getUser () {
			return currentUser;
		},
		setUser (user) {
			currentUser = user;
		}
	}
}
]);