import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  currentDevice: "isDesktop",// "isDesktop", "isIpad", "isMobile"
  touchDevice: false
};

const deviceSlice = createSlice({
  name: 'device',
  initialState,
  reducers: {
    setCurrentDevice: (state, action) => {
      state.currentDevice = action.payload;
    },
    setTouchDevice: (state, action) => {
      state.touchDevice = action.payload;
    }
  }
});

export const Device = deviceSlice.reducer;
export const DEVICE_ACTIONS = deviceSlice.actions;