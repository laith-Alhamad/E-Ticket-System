import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// إعداد Vite للـ React بـ JavaScript العادي
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
  },
})
