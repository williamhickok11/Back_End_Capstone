"use strict";

SwapNShop.factory('RentalRequestNote', [

function () {

	let RentalRequestNotificationId = null;

	return {
		getRRNotificationId () {
			return RentalRequestNotificationId;
		},
		setRRNotificationId (id) {
			RentalRequestNotificationId = id;
		}
	}
}
]);