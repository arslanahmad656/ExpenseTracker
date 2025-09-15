import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  username: null,
  role: null,
  isLoggedIn: false,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login: (state, action) => {
      const { username, role } = action.payload;
      state.username = username;
      state.role = role;
      state.isLoggedIn = true;
    },
    logout: (state) => {
      state.username = null;
      state.role = null;
      state.isLoggedIn = false;
    },
  },
});

export const { login, logout } = authSlice.actions;
export default authSlice.reducer;
