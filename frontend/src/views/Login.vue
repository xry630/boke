<template>
  <div class="auth-container">
    <div class="auth-card">
      <h1 class="auth-title">欢迎回来</h1>
      <p class="auth-subtitle">登录到视频管理系统</p>

      <div v-if="error" class="error-message">
        {{ error }}
      </div>

      <form @submit.prevent="handleLogin">
        <div class="form-group">
          <label class="form-label">用户名</label>
          <input
            v-model="form.username"
            type="text"
            class="form-input"
            placeholder="请输入用户名"
            required
            :disabled="loading"
          />
        </div>

        <div class="form-group">
          <label class="form-label">密码</label>
          <input
            v-model="form.password"
            type="password"
            class="form-input"
            placeholder="请输入密码"
            required
            :disabled="loading"
          />
        </div>

        <button type="submit" class="btn btn-primary" :disabled="loading">
          <span v-if="loading" class="loading"></span>
          <span v-else>登录</span>
        </button>
      </form>

      <div class="auth-footer">
        还没有账号？
        <router-link to="/register" class="auth-link">立即注册</router-link>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import { authApi } from '@/api/auth'

const router = useRouter()
const authStore = useAuthStore()

const form = ref({
  username: '',
  password: ''
})

const loading = ref(false)
const error = ref('')

const handleLogin = async () => {
  error.value = ''
  loading.value = true

  try {
    const response = await authApi.login({
      username: form.value.username,
      password: form.value.password
    })
    
    console.log('Login response:', response, 'Type:', typeof response)

    // Handle different response formats
    let token = response
    if (typeof response === 'object' && response !== null) {
      // If response is an object, try to extract token from common properties
      token = response.token || response.data || response.accessToken || response.jwt
    }
    
    console.log('Extracted token:', token, 'Type:', typeof token)

    if (token && typeof token === 'string') {
      authStore.setToken(token)
      router.push('/')
    } else {
      throw new Error('Invalid token received from server')
    }
  } catch (err) {
    error.value = err.message || '登录失败，请检查用户名和密码'
  } finally {
    loading.value = false
  }
}
</script>
