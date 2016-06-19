"use strict";

SwapNShop.factory('SelectedUserFactory', [

function () {

	let currentUser = null;

	return {
		getUserId () {
			return currentUser;
		},
		setUserId (id) {
			currentUser = id;
		}
	}
}
]);