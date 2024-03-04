import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  currentPageKey: '',
  currentPageSubKey: '',
  loading: false,
  prevPageKey: '',
  params: null,
  locationParams: [],
  profile: null,
  currentRole: null
  // statusForTitle:null,
  // roleId:null,
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
    setParams: (state, action)=>{
      state.params = action.payload.params;
      // state.statusForTitle = action.payload.statusForTitle;
      // state.roleId = action.payload.roleId;

    },
    setRole: (state, action) =>{
      state.currentRole = action.payload;
    },
    setLocationParams: (state, action) =>{
      state.locationParams = action.payload.locationParams;
    },
    onPageReady: (state, action) => {
      state.loading = false;
    },
    showPreloader: (state, action) => {
      state.loading = true;
    },
    setProfile: (state, action) =>{
      state.profile = action.payload;
    }
  }
});

export const Page = pageSlice.reducer;
export const PAGE_ACTIONS = pageSlice.actions;