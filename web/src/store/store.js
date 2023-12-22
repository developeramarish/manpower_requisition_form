import { configureStore } from '@reduxjs/toolkit';
import { Device } from '../reducers/Device_r.js';
import { Page } from '../reducers/Page_r.js';

const store = configureStore({
  reducer: {
    page: Page,
    device: Device
  }
});

export default store;