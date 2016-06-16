"use strict";

SwapNShop.factory('EquipFactory', [

	function () {

		let currentEquipID = null;

		return {
			getEquipment () {
				return currentEquipID;
			},
			setEquipment (equiID) {
				currentEquipID = equiID;
			}
		}
	}
]);