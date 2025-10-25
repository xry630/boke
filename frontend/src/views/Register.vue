<template>
  <div class="auth-container">
    <div class="auth-card">
      <h1 class="auth-title">创建账号</h1>
      <p class="auth-subtitle">注册视频管理系统账号</p>

      <div v-if="error" class="error-message">
        {{ error }}
      </div>

      <div v-if="success" class="success-message">
        {{ success }}
      </div>

      <form @submit.prevent="handleRegister">
        <div class="form-group">
          <label class="form-label">昵称</label>
          <input
            v-model="form.name"
            type="text"
            class="form-input"
            placeholder="请输入昵称"
            required
            :disabled="loading"
          />
        </div>

        <div class="form-group">
          <label class="form-label">用户名</label>
          <input
            v-model="form.username"
            type="text"
            class="form-input"
            placeholder="请输入用户名（用于登录）"
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

        <div class="form-group">
          <label class="form-label">确认密码</label>
          <input
            v-model="confirmPassword"
            type="password"
            class="form-input"
            placeholder="请再次输入密码"
            required
            :disabled="loading"
          />
        </div>

        <div class="form-group">
          <label class="form-label">验证码</label>
          <div class="code-group">
            <input
              v-model="form.code"
              type="text"
              class="form-input"
              placeholder="请输入验证码"
              required
              :disabled="loading"
            />
            <button
              type="button"
              class="btn btn-secondary btn-code"
              :disabled="codeLoading || countdown > 0 || !form.username"
              @click="getVerificationCode"
            >
              <span v-if="codeLoading" class="loading"></span>
              <span v-else-if="countdown > 0">{{ countdown }}秒后重试</span>
              <span v-else>获取验证码</span>
            </button>
          </div>
        </div>

        <button type="submit" class="btn btn-primary" :disabled="loading">
          <span v-if="loading" class="loading"></span>
          <span v-else>注册</span>
        </button>
      </form>

      <div class="auth-footer">
        已有账号？
        <router-link to="/login" class="auth-link">立即登录</router-link>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import { authApi } from '@/api/auth'
import { codeApi } from '@/api/code'

const router = useRouter()
const authStore = useAuthStore()

const form = ref({
  name: '',
  username: '',
  password: '',
  code: '',
  avatar: ''
})

const confirmPassword = ref('')
const loading = ref(false)
const codeLoading = ref(false)
const error = ref('')
const success = ref('')
const countdown = ref(0)

let timer = null

const getVerificationCode = async () => {
  if (!form.value.username) {
    error.value = '请先输入用户名'
    return
  }

  error.value = ''
  success.value = ''
  codeLoading.value = true

  try {
    const code = await codeApi.getCode({
      value: form.value.username,
      type: 0
    })

    success.value = `验证码已发送：${code}`
    
    countdown.value = 60
    timer = setInterval(() => {
      countdown.value--
      if (countdown.value <= 0) {
        clearInterval(timer)
      }
    }, 1000)
  } catch (err) {
    error.value = err.message || '获取验证码失败'
  } finally {
    codeLoading.value = false
  }
}

const handleRegister = async () => {
  error.value = ''
  success.value = ''

  if (form.value.password !== confirmPassword.value) {
    error.value = '两次输入的密码不一致'
    return
  }

  if (!form.value.code) {
    error.value = '请输入验证码'
    return
  }

  loading.value = true

  try {
    const token = await authApi.register({
      name: form.value.name,
      username: form.value.username,
      password: form.value.password,
      code: form.value.code,
      avatar: form.value.avatar
    })

    authStore.setToken(token)
    router.push('/')
  } catch (err) {
    error.value = err.message || '注册失败，请重试'
  } finally {
    loading.value = false
  }
}
</script>
