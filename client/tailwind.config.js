// File      : client/tailwind.config.js
// Developer : Saeid El-Kholy (M4)
// Reviewer  : Ayman Ouf (M5)
// Task(s)   : T015  -  Design system
// Module    : Foundation (FSD §2-8, §14-15)
// Purpose   : Purple design tokens (FSD §6.1 / §6.3). No blue in the interface.

export default {
  content: ['./index.html', './src/**/*.{js,jsx}'],
  theme: {
    extend: {
      colors: {
        brand: { 50:'#F5F3FF',100:'#EDE9FE',200:'#DDD6FE',300:'#C4B5FD',400:'#A78BFA',
                 500:'#8B5CF6',600:'#7C3AED',700:'#6D28D9',800:'#5B21B6',900:'#4C1D95',950:'#2E1065' },
        accent: '#A21CAF',
      },
      fontFamily: { sans: ['Inter', 'Cairo', 'sans-serif'] },
      borderRadius: { xl: '12px' },
    },
  },
};
