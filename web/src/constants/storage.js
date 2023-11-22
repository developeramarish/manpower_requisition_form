import { APP_KEY } from "./config";

/* Store Service start*/

export const storageService = {
    store: window.localStorage,
    key: APP_KEY,
    init: function () {
        this.store.setItem(this.key, "");
    },
    setData: function (key, value) {
        if (!this.store) {
            console.error("ERROR: Storage not available.")
            return;
        }
        var currentData = this.validateData();
        currentData[key] = value;
        this.store.setItem(this.key, JSON.stringify(currentData));
    },
    getData: function (key) {
        if (!this.store) {
            console.error("ERROR: Storage not available.")
            return;
        }
        var currentData = (this.store && this.store.getItem(this.key) !== "") ? JSON.parse(this.store.getItem(this.key)) : undefined;
        if (currentData === undefined) { return; }
        return currentData && currentData[key];
    },
    hasData(key) {
        if (!this.store) {
            console.error("ERROR: Storage not available.")
            return;
        }
        let currentData = this.validateData();
        return currentData && currentData.hasOwnProperty(key);
    },
    removeData(key) {
        if (!this.store) {
            console.error("ERROR: Storage not available.")
            return;
        }
        let currentData = JSON.parse(this.store.getItem(this.key));
        if (currentData) {
            delete currentData[key];
            this.store.setItem(this.key, JSON.stringify(currentData));
        }
    },
    clearData() {
        let currentData = (this.store) ? JSON.parse(this.store.getItem(this.key)) : undefined;
        if (currentData === undefined) {
            console.error("ERROR: DATA not available in STORAGE.")
            return;
        }
        this.store.removeItem(this.key);
        this.store.clear();
    },
    validateData() {
        let currentData = this.store.getItem(this.key);
        if (currentData === "") {
            currentData = {};
        } else {
            currentData = JSON.parse(currentData);
        }

        return currentData;
    }
}

/* Store Service End */