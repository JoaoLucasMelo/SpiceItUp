import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  recipes: [],
  activeIngredients:[],
  activeSteps:[],
  favorites:[],
  menu: 1,
  categories: ["Pasta", "Burguer", "Salad", "Soup", "Fish", "Beef", "Other"]
})
