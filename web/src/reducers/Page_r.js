import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  currentPageKey: '',
  currentPageSubKey: '',
  loading: false,
  prevPageKey: ''
};

const pageSlice = createSlice({
  name: 'page',
  initialState,
  reducers: {
    setCurrentPageKey: (state, action) => {
      state.prevPageKey = state.currentPageKey;
      state.loading = (state.currentPageKey === action.payload.pageKey && state.currentPageSubKey === action.payload.pageSubKey) ? false : true;
      state.currentPageKey = action.payload.pageKey;
      state.currentPageSubKey = action.payload.pageSubKey;
      // console.log('Page Reducer # Current Page Key: ', state.currentPageKey, ' | Page Sybkey: ', state.currentPageSubKey);
    },
    onPageReady: (state, action) => {
      state.loading = false;
    },
    showPreloader: (state, action) => {
      state.loading = true;
    }
  }
});

export const Page = pageSlice.reducer;
export const PAGE_ACTIONS = pageSlice.actions;